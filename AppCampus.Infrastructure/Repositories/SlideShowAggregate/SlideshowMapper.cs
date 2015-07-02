using AppCampus.Domain.Models.Entities;
using AppCampus.Domain.Models.ValueObjects;
using AppCampus.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drumble.DomainDrivenArchitecture.Infrastructure.EntityFramework;

namespace AppCampus.Infrastructure.Repositories.SlideshowAggregate
{
    public class SlideshowMapper : IEntityMapper<Slideshow, SlideshowTable>
    {
        public Slideshow CreateFrom(SlideshowTable dataEntity)
        {
            if (dataEntity == null)
            {
                throw new ArgumentNullException("dataEntity");
            }

            var slideshow = Slideshow.Hydrate(dataEntity.Id, dataEntity.Name, dataEntity.CompanyId, dataEntity.IsDeleted);
            
            foreach(SlideTable slideTable in dataEntity.Slides.Where(s => !s.IsDeleted).OrderBy(s => s.OrderNumber))
            {
                Slide slide = Slide.Hydrate(
                    slideTable.Id, 
                    slideTable.BackgroundColour == null ? new Colour("#000000") : new Colour(slideTable.BackgroundColour), 
                    Duration.From(slideTable.Duration), 
                    Transition.From(slideTable.Transition), 
                    slideTable.Name, 
                    slideTable.IsActive, 
                    slideTable.IsDeleted);

                foreach (SlideWidgetTable slideWidgetTable in slideTable.SlideWidgets) 
                {
                    Widget widget = new Widget(slideWidgetTable.Id, slideWidgetTable.WidgetDefinitionId, WidgetLayout.From(1, 1));
 
                    foreach(ParameterTable parameterValueTable in slideWidgetTable.Parameters)
                    {
                        widget.AssignParameter(Parameter.From(parameterValueTable.ParameterDefinitionId, parameterValueTable.Value));         
                    }

                    slide.AddWidget(widget);
                }
            
                slideshow.Insert(slide);
            }

            return slideshow;
        }

        public SlideshowTable CreateFrom(Slideshow domainEntity)
        {
            if (domainEntity == null)
            {
                throw new ArgumentNullException("domainEntity");
            }

            int slideIndex = 0;
            return new SlideshowTable()
            {
                Id = domainEntity.Id,
                Name = domainEntity.Name,
                CompanyId = domainEntity.CompanyId,
                CreatedDate = DateTime.UtcNow,
                IsDeleted = domainEntity.IsDeleted,
                Slides = domainEntity.Slides.Select(slide =>
                    new SlideTable()
                    {
                        Id = slide.Id,
                        OrderNumber = slideIndex++,
                        SlideshowId = domainEntity.Id,
                        Duration = slide.Duration.Seconds,
                        Transition = slide.Transition.Type.ToString(),
                        Name = slide.Name,
                        BackgroundColour = slide.BackgroundColour.ColourHex,
                        IsActive = slide.IsActive,
                        IsDeleted = slide.IsDeleted,
                        SlideWidgets = slide.Widgets.Select(widget =>
                            new SlideWidgetTable()
                            {
                                Id = widget.Id,
                                SlideId = slide.Id,
                                WidgetDefinitionId = widget.WidgetDefinitionId,
                                Parameters = widget.Parameters.Select(parameter => 
                                    new ParameterTable() 
                                    {
                                        Id = Guid.NewGuid(),
                                        SlideWidgetId = widget.Id,
                                        ParameterDefinitionId = parameter.ParameterDefinitionId,
                                        Value = parameter.Value
                                    }).ToList()
                            }).ToList()
                    }).ToList()
            };
        }
    }
}
